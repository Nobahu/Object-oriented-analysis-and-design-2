package Repository;

import Model.CaCertificateRepository;
import Model.UserCertificateRepository;
import Model.X509CaCertificateData;
import Model.X509UserCertificateData;

import java.security.KeyFactory;
import java.security.PrivateKey;
import java.security.spec.PKCS8EncodedKeySpec;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

public class PgCertificateRepository implements CaCertificateRepository, UserCertificateRepository {

    private final String url;
    private final String user;
    private final String password;

    public PgCertificateRepository(String host, int port, String dbName,
                                   String user, String password) {
        this.url = String.format("jdbc:postgresql://%s:%d/%s", host, port, dbName);
        this.user = user;
        this.password = password;
    }

    private Connection getConnection() throws SQLException {
        return DriverManager.getConnection(url, user, password);
    }

    // ==================== CA ====================

    @Override
    public void saveCa(X509CaCertificateData ca) {
        String sqlCa = """
        INSERT INTO ca_certificates (subject_name, not_before, not_after, 
                                    algorithm, public_key_info, certificate_bytes)
        VALUES (?, ?, ?, ?, ?, ?)
        RETURNING id
        """;

        String sqlKey = """
        INSERT INTO ca_key (ca_id, private_key_info)
        VALUES (?, ?)
        """;

        try (Connection conn = getConnection()) {
            conn.setAutoCommit(false);  // транзакция — либо оба запроса, либо ни одного

            try (PreparedStatement stmtCa = conn.prepareStatement(sqlCa)) {
                stmtCa.setString(1, ca.subjectName());
                stmtCa.setTimestamp(2, Timestamp.valueOf(ca.notBefore()));
                stmtCa.setTimestamp(3, Timestamp.valueOf(ca.notAfter()));
                stmtCa.setString(4, ca.algorithm());
                stmtCa.setString(5, ca.publicKeyInfo());
                stmtCa.setBytes(6, ca.certificateBytes());

                ResultSet rs = stmtCa.executeQuery();
                rs.next();
                int caId = rs.getInt(1);  // получаем ID созданного CA

                // Сохраняем приватный ключ
                try (PreparedStatement stmtKey = conn.prepareStatement(sqlKey)) {
                    stmtKey.setInt(1, caId);
                    stmtKey.setBytes(2, ca.getPrivateKey().getEncoded());
                    stmtKey.executeUpdate();
                }

                conn.commit();
                System.out.println("[PostgreSQL] CA + ключ сохранены");
            } catch (SQLException e) {
                conn.rollback();
                throw e;
            }

        } catch (SQLException e) {
            throw new RuntimeException("Failed to save CA with key", e);
        }
    }


    @Override
    public Optional<X509CaCertificateData> findCaById(int id) {
        String sql = "SELECT * FROM ca_certificates WHERE id = ?";
        try (Connection conn = getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setInt(1, id);
            ResultSet rs = stmt.executeQuery();
            if (rs.next()) {
                return Optional.of(mapToCa(rs));
            }
            return Optional.empty();
        } catch (SQLException e) {
            throw new RuntimeException("Failed to find CA", e);
        }
    }

    @Override
    public List<X509CaCertificateData> findAllCa() {
        String sql = "SELECT * FROM ca_certificates";
        List<X509CaCertificateData> result = new ArrayList<>();
        try (Connection conn = getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {
            while (rs.next()) {
                result.add(mapToCa(rs));
            }
        } catch (SQLException e) {
            throw new RuntimeException("Failed", e);
        }
        return result;
    }

    private X509CaCertificateData mapToCa(ResultSet rs) throws SQLException {
        int caId = rs.getInt("id");
        PrivateKey privateKey = loadPrivateKey(caId);  // грузим ключ

        return new X509CaCertificateData(
                rs.getString("subject_name"),
                rs.getTimestamp("not_before").toLocalDateTime(),
                rs.getTimestamp("not_after").toLocalDateTime(),
                rs.getString("algorithm"),
                rs.getString("public_key_info"),
                rs.getBytes("certificate_bytes"),
                privateKey
        );
    }

    private PrivateKey loadPrivateKey(int caId) {
        String sql = "SELECT private_key_info FROM ca_key WHERE ca_id = ?";
        try (Connection conn = getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setInt(1, caId);
            ResultSet rs = stmt.executeQuery();
            if (rs.next()) {
                byte[] keyBytes = rs.getBytes("private_key_info");
                return KeyFactory.getInstance("RSA").generatePrivate(new PKCS8EncodedKeySpec(keyBytes));
            }
            return null;
        } catch (Exception e) {
            throw new RuntimeException("Failed to load private key", e);
        }
    }

    // ==================== USER ====================

    @Override
    public void saveUser(X509UserCertificateData userCert) {
        String sql = """
            INSERT INTO user_certificates (subject_name, issuer_name, not_before, 
                                          not_after, algorithm, public_key_info, certificate_bytes)
            VALUES (?, ?, ?, ?, ?, ?, ?)
            """;

        try (Connection conn = getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {

            stmt.setString(1, userCert.subjectName());
            stmt.setString(2, userCert.issuerName());
            stmt.setTimestamp(3, Timestamp.valueOf(userCert.notBefore()));
            stmt.setTimestamp(4, Timestamp.valueOf(userCert.notAfter()));
            stmt.setString(5, userCert.algorithm());
            stmt.setString(6, userCert.publicKeyInfo());
            stmt.setBytes(7, userCert.certificateBytes());
            stmt.executeUpdate();
            System.out.println("[PostgreSQL] User сертификат сохранен");

        } catch (SQLException e) {
            throw new RuntimeException("Failed to save user certificate", e);
        }
    }

    @Override
    public Optional<X509UserCertificateData> findUserById(int id) {
        String sql = "SELECT * FROM user_certificates WHERE id = ?";
        try (Connection conn = getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setInt(1, id);
            ResultSet rs = stmt.executeQuery();
            if (rs.next()) {
                return Optional.of(mapToUser(rs));
            }
            return Optional.empty();
        } catch (SQLException e) {
            throw new RuntimeException("Failed", e);
        }
    }

    @Override
    public List<X509UserCertificateData> findAllUsers() {
        String sql = "SELECT * FROM user_certificates";
        List<X509UserCertificateData> result = new ArrayList<>();
        try (Connection conn = getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {
            while (rs.next()) {
                result.add(mapToUser(rs));
            }
        } catch (SQLException e) {
            throw new RuntimeException("Failed", e);
        }
        return result;
    }

    private X509UserCertificateData mapToUser(ResultSet rs) throws SQLException {
        return new X509UserCertificateData(
                rs.getString("subject_name"),
                rs.getString("issuer_name"),
                rs.getTimestamp("not_before").toLocalDateTime(),
                rs.getTimestamp("not_after").toLocalDateTime(),
                rs.getString("algorithm"),
                rs.getString("public_key_info"),
                rs.getBytes("certificate_bytes")
        );
    }
}
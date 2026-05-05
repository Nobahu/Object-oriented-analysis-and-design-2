package Model;

import java.util.List;
import java.util.Optional;

public interface UserCertificateRepository {
    void saveUser(X509UserCertificateData certificate);
    Optional<X509UserCertificateData> findUserById(int id);
    List<X509UserCertificateData> findAllUsers();
}

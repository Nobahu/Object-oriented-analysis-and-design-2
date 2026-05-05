package Repository;

import Model.CaCertificateRepository;
import Model.UserCertificateRepository;
import Model.X509CaCertificateData;
import Model.X509UserCertificateData;

import java.util.*;

public class FakeCertificateRepository implements CaCertificateRepository, UserCertificateRepository {

    private final Map<Integer, X509CaCertificateData> caStorage = new LinkedHashMap<>();
    private final Map<Integer, X509UserCertificateData> userStorage = new LinkedHashMap<>();
    private int caNextId = 1;
    private int userNextId = 1;

    // ==================== CA ====================

    @Override
    public void saveCa(X509CaCertificateData certificate) {
        caStorage.put(caNextId++, certificate);
        System.out.println("[FAKE DB] CA сохранен. Всего CA: " + caStorage.size());
    }

    @Override
    public Optional<X509CaCertificateData> findCaById(int id) {
        return Optional.ofNullable(caStorage.get(id));
    }

    @Override
    public List<X509CaCertificateData> findAllCa() {
        return new ArrayList<>(caStorage.values());
    }

    // ==================== USER ====================

    @Override
    public void saveUser(X509UserCertificateData certificate) {
        userStorage.put(userNextId++, certificate);
        System.out.println("[FAKE DB] User сохранен. Всего User: " + userStorage.size());
    }

    @Override
    public Optional<X509UserCertificateData> findUserById(int id) {
        return Optional.ofNullable(userStorage.get(id));
    }

    @Override
    public List<X509UserCertificateData> findAllUsers() {
        return new ArrayList<>(userStorage.values());
    }

    // ==================== ОТЛАДКА ====================

    public int caSize() {
        return caStorage.size();
    }

    public int userSize() {
        return userStorage.size();
    }

    public int totalSize() {
        return caStorage.size() + userStorage.size();
    }

    public void clear() {
        caStorage.clear();
        userStorage.clear();
        caNextId = 1;
        userNextId = 1;
        System.out.println("[FAKE DB] Хранилище очищено");
    }
}

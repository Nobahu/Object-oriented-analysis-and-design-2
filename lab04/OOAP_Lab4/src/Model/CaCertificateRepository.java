package Model;

import java.util.List;
import java.util.Optional;

public interface CaCertificateRepository {
    void saveCa(X509CaCertificateData certificate);
    Optional<X509CaCertificateData> findCaById(int id);
    List<X509CaCertificateData> findAllCa();
}


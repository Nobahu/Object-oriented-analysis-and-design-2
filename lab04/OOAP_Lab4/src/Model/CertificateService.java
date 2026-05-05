package Model;

public interface CertificateService {
    X509CaCertificateData createCA(String cn, String org, String country, int days);
    X509UserCertificateData createUser(String cn, String org, String country, int days, X509CaCertificateData ca);
    int caCount();
    int userCount();
}

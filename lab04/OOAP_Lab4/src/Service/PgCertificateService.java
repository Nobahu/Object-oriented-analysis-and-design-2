package Service;

import Model.*;
import Repository.PgCertificateRepository;

public class PgCertificateService implements CertificateService {
    private final X509CertificateGenerator generator;
    private final PgCertificateRepository repository;

    public PgCertificateService(String host, int port, String db, String user, String pass) {
        this.generator = new X509CertificateGenerator();
        this.repository = new PgCertificateRepository(host, port, db, user, pass);
    }

    @Override
    public X509CaCertificateData createCA(String cn, String org, String country, int days) {
        X509CaCertificateData ca = generator.generateCA(cn, org, country, days);
        repository.saveCa(ca);
        return ca;
    }

    @Override
    public X509UserCertificateData createUser(String cn, String org, String country, int days, X509CaCertificateData ca) {
        X509UserCertificateData user = generator.generateUser(cn, org, country, days, ca);
        repository.saveUser(user);
        return user;
    }

    @Override
    public int caCount() { return repository.findAllCa().size(); }

    @Override
    public int userCount() { return repository.findAllUsers().size(); }
}
package Service;

import Model.*;
import Repository.FakeCertificateRepository;

public class FakeCertificateService implements CertificateService {
    private final X509CertificateGenerator generator;
    private final FakeCertificateRepository repository;

    public FakeCertificateService() {
        this.generator = new X509CertificateGenerator();
        this.repository = new FakeCertificateRepository();
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
    public int caCount() { return repository.caSize(); }

    @Override
    public int userCount() { return repository.userSize(); }

    public void clear() { repository.clear(); }
}
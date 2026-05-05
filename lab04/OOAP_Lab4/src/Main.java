import Model.CertificateService;
import Model.X509CaCertificateData;
import Model.X509CertificateGenerator;
import Model.X509UserCertificateData;
import Service.FakeCertificateService;
import Service.PgCertificateService;

public class Main {
    public static void main(String[] args) {
        // Проверяем аргументы командной строки
        boolean debug = false;
        for (String arg : args) {
            if (arg.equalsIgnoreCase("--debug")) {
                debug = true;
                break;
            }
        }

        // Выбор службы
        CertificateService service;
        if (debug) {
            System.out.println("=== ЗАПУСК С ФИКТИВНОЙ СЛУЖБОЙ (HashMap) ===\n");
            service = new FakeCertificateService();
        } else {
            System.out.println("=== ЗАПУСК С POSTGRESQL ===\n");
            service = new PgCertificateService("localhost", 5433, "certdb", "certuser", "certpass");
        }

        X509CertificateGenerator generator = new X509CertificateGenerator();

        X509CaCertificateData rootCA = service.createCA("CertAuth", "Lyamburtsev.Ltd", "RU", 3650);
        X509UserCertificateData user = service.createUser("TSUUser", "TSU", "RU", 365, rootCA);


        System.out.println("\n=== РЕЗУЛЬТАТЫ ===");
        System.out.println("CA: " + service.caCount());
        System.out.println("Users: " + service.userCount());
        System.out.println("RootCA: " + rootCA.subjectName());
        System.out.println("User: " + user.subjectName());
        System.out.println("User issued by: " + user.issuerName());
    }
}

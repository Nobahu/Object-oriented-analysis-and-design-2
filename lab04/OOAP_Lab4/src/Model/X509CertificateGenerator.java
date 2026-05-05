package Model;

import org.bouncycastle.asn1.x500.X500Name;
import org.bouncycastle.cert.X509v3CertificateBuilder;
import org.bouncycastle.cert.jcajce.JcaX509CertificateConverter;
import org.bouncycastle.cert.jcajce.JcaX509v3CertificateBuilder;
import org.bouncycastle.jce.provider.BouncyCastleProvider;
import org.bouncycastle.operator.ContentSigner;
import org.bouncycastle.operator.jcajce.JcaContentSignerBuilder;

import java.math.BigInteger;
import java.security.*;
import java.security.cert.X509Certificate;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.util.Date;

public class X509CertificateGenerator {

    static
    {
        // Регистрируем Bouncy Castle один раз при загрузке класса
        Security.addProvider(new BouncyCastleProvider());
    }

    public X509CaCertificateData generateCA(String commonName, String org, String country, int validDays) {
        try {
            KeyPairGenerator keyGen = KeyPairGenerator.getInstance("RSA", "BC");
            keyGen.initialize(4096);
            KeyPair keyPair = keyGen.generateKeyPair();

            String subjectDN = String.format("CN=%s, O=%s, C=%s", commonName, org, country);
            X500Name subject = new X500Name(subjectDN);

            Date notBefore = new Date();
            Date notAfter = new Date(System.currentTimeMillis() + validDays * 24L * 60 * 60 * 1000);

            X509v3CertificateBuilder certBuilder = new JcaX509v3CertificateBuilder(
                    subject,
                    BigInteger.valueOf(System.currentTimeMillis()),
                    notBefore,
                    notAfter,
                    subject,
                    keyPair.getPublic()
            );

            ContentSigner signer = new JcaContentSignerBuilder("SHA256WithRSA")
                    .setProvider("BC")
                    .build(keyPair.getPrivate());

            X509Certificate certificate = new JcaX509CertificateConverter()
                    .setProvider("BC")
                    .getCertificate(certBuilder.build(signer));

            PublicKey publicKey = keyPair.getPublic();
            String publicKeyInfo = String.format(
                    "%s %d bit",
                    publicKey.getAlgorithm(),
                    ((java.security.interfaces.RSAPublicKey) publicKey).getModulus().bitLength()
            );

            return new X509CaCertificateData(
                    subjectDN,
                    LocalDateTime.ofInstant(notBefore.toInstant(), ZoneId.systemDefault()),
                    LocalDateTime.ofInstant(notAfter.toInstant(), ZoneId.systemDefault()),
                    "SHA256WithRSA",
                    publicKeyInfo,
                    certificate.getEncoded(),
                    keyPair.getPrivate()   // ← приватный ключ
            );

        } catch (Exception e) {
            throw new RuntimeException("Failed to generate CA certificate", e);
        }
    }

    public X509UserCertificateData generateUser(
            String commonName, String org, String country,
            int validDays, X509CaCertificateData ca) {
        try {
            KeyPairGenerator keyGen = KeyPairGenerator.getInstance("RSA", "BC");
            keyGen.initialize(2048);
            KeyPair userKeyPair = keyGen.generateKeyPair();

            String subjectDN = String.format("CN=%s, O=%s, C=%s", commonName, org, country);
            X500Name subject = new X500Name(subjectDN);
            X500Name issuer = new X500Name(ca.subjectName());

            Date notBefore = new Date();
            Date notAfter = new Date(System.currentTimeMillis() + validDays * 24L * 60 * 60 * 1000);

            X509v3CertificateBuilder certBuilder = new JcaX509v3CertificateBuilder(
                    issuer,
                    BigInteger.valueOf(System.currentTimeMillis()),
                    notBefore,
                    notAfter,
                    subject,
                    userKeyPair.getPublic()
            );

            // Подписываем приватным ключом CA
            ContentSigner signer = new JcaContentSignerBuilder("SHA256WithRSA")
                    .setProvider("BC")
                    .build(ca.getPrivateKey());

            X509Certificate certificate = new JcaX509CertificateConverter()
                    .setProvider("BC")
                    .getCertificate(certBuilder.build(signer));

            PublicKey publicKey = userKeyPair.getPublic();
            String publicKeyInfo = String.format(
                    "%s %d bit",
                    publicKey.getAlgorithm(),
                    ((java.security.interfaces.RSAPublicKey) publicKey).getModulus().bitLength()
            );

            return new X509UserCertificateData(
                    subjectDN,
                    ca.subjectName(),
                    LocalDateTime.ofInstant(notBefore.toInstant(), ZoneId.systemDefault()),
                    LocalDateTime.ofInstant(notAfter.toInstant(), ZoneId.systemDefault()),
                    "SHA256WithRSA",
                    publicKeyInfo,
                    certificate.getEncoded()
            );

        } catch (Exception e) {
            throw new RuntimeException("Failed to generate user certificate", e);
        }
    }

}

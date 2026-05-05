package Model;

import java.time.LocalDateTime;
import java.util.Arrays;

public class X509UserCertificateData {
    private final String subjectName_;
    private final String issuerName_;
    private final LocalDateTime notBefore_;
    private final LocalDateTime notAfter_;
    private final String algorithm_;
    private final String publicKeyInfo_;
    private final byte[] certificateBytes_;

    public X509UserCertificateData(
            String subjectName,
            String issuerName,
            LocalDateTime notBefore,
            LocalDateTime notAfter,
            String algorithm,
            String publicKeyInfo,
            byte[] certificateBytes) {

        if (subjectName == null || subjectName.isBlank()) {
            throw new IllegalArgumentException("subjectName cannot be null or empty");
        }
        if (issuerName == null || issuerName.isBlank()) {
            throw new IllegalArgumentException("issuerName cannot be null or empty");
        }
        if (notBefore == null) {
            throw new IllegalArgumentException("notBefore cannot be null");
        }
        if (notAfter == null) {
            throw new IllegalArgumentException("notAfter cannot be null");
        }
        if (notAfter.isBefore(notBefore)) {
            throw new IllegalArgumentException("notAfter must be after notBefore");
        }
        if (algorithm == null || algorithm.isBlank()) {
            throw new IllegalArgumentException("algorithm cannot be null or empty");
        }
        if (publicKeyInfo == null || publicKeyInfo.isBlank()) {
            throw new IllegalArgumentException("publicKeyInfo cannot be null or empty");
        }
        if (certificateBytes == null || certificateBytes.length == 0) {
            throw new IllegalArgumentException("certificateBytes cannot be null or empty");
        }

        this.subjectName_ = subjectName;
        this.issuerName_ = issuerName;
        this.notBefore_ = notBefore;
        this.notAfter_ = notAfter;
        this.algorithm_ = algorithm;
        this.publicKeyInfo_ = publicKeyInfo;
        this.certificateBytes_ = Arrays.copyOf(certificateBytes, certificateBytes.length);
    }

    public String subjectName() { return subjectName_; }
    public String issuerName() { return issuerName_; }
    public LocalDateTime notBefore() { return notBefore_; }
    public LocalDateTime notAfter() { return notAfter_; }
    public String algorithm() { return algorithm_; }
    public String publicKeyInfo() { return publicKeyInfo_; }
    public byte[] certificateBytes() { return Arrays.copyOf(certificateBytes_, certificateBytes_.length); }

    public boolean isExpired() { return LocalDateTime.now().isAfter(notAfter_); }

    public boolean isValid() {
        LocalDateTime now = LocalDateTime.now();
        return now.isAfter(notBefore_) && now.isBefore(notAfter_);
    }

    @Override
    public String toString() {
        return String.format(
                "UserCertificate[Subject=%s, Issuer=%s, Valid=%s - %s, Algorithm=%s, Size=%d bytes]",
                subjectName_, issuerName_, notBefore_, notAfter_, algorithm_, certificateBytes_.length
        );
    }
}

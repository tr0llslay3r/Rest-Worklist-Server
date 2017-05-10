using Dicom;

namespace DicomCore
{
    public class TransferSyntaxesHelper
    {
        public static DicomTransferSyntax[] AcceptedTransferSyntaxes = {
                                                                        DicomTransferSyntax
                                                                            .ExplicitVRLittleEndian,
                                                                        DicomTransferSyntax
                                                                            .ExplicitVRBigEndian,
                                                                        DicomTransferSyntax
                                                                            .ImplicitVRLittleEndian
                                                                            };

        public static DicomTransferSyntax[] AcceptedImageTransferSyntaxes = {
                                                                                // Lossless
                                                                                DicomTransferSyntax
                                                                                    .JPEGLSLossless,
                                                                                DicomTransferSyntax
                                                                                    .JPEG2000Lossless,
                                                                                DicomTransferSyntax
                                                                                    .JPEGProcess14SV1,
                                                                                DicomTransferSyntax
                                                                                    .JPEGProcess14,
                                                                                DicomTransferSyntax
                                                                                    .RLELossless,

                                                                                // Lossy
                                                                                DicomTransferSyntax
                                                                                    .JPEGLSNearLossless,
                                                                                DicomTransferSyntax
                                                                                    .JPEG2000Lossy,
                                                                                DicomTransferSyntax
                                                                                    .JPEGProcess1,
                                                                                DicomTransferSyntax
                                                                                    .JPEGProcess2_4,

                                                                                // Uncompressed
                                                                                DicomTransferSyntax
                                                                                    .ExplicitVRLittleEndian,
                                                                                DicomTransferSyntax
                                                                                    .ExplicitVRBigEndian,
                                                                                DicomTransferSyntax
                                                                                    .ImplicitVRLittleEndian
                                                                                 };
    }
}

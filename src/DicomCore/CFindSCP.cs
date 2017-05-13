using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dicom;
using Dicom.Log;
using Dicom.Network;

namespace DicomCore
{
    public class CFindSCP : DicomService, IDicomServiceProvider, IDicomCEchoProvider, IDicomCFindProvider, IDicomNServiceProvider

    {
        private readonly WorklistRepository m_WorklistRepository;
        public CFindSCP(INetworkStream stream, Encoding fallbackEncoding, Logger log)
            : base(stream, fallbackEncoding, log)
        {
            m_WorklistRepository = new WorklistRepository();
        }

        public void OnReceiveAssociationRequest(DicomAssociation association)
        {
            // TODO make this AE Title configurable via command line parameter or config
            //if (association.CalledAE != "TEST_AE_WORKLIST")
            //{
            //    SendAssociationReject(
            //        DicomRejectResult.Permanent,
            //        DicomRejectSource.ServiceUser,
            //        DicomRejectReason.CalledAENotRecognized);
            //    return;
            //}
            
            foreach (var pc in association.PresentationContexts)
            {
                if (pc.AbstractSyntax == DicomUID.ModalityWorklistInformationModelFIND) pc.AcceptTransferSyntaxes(TransferSyntaxesHelper.AcceptedTransferSyntaxes);
                if (pc.AbstractSyntax == DicomUID.ModalityPerformedProcedureStepSOPClass) pc.AcceptTransferSyntaxes(TransferSyntaxesHelper.AcceptedTransferSyntaxes);
                else if (pc.AbstractSyntax.StorageCategory != DicomStorageCategory.None) pc.AcceptTransferSyntaxes(TransferSyntaxesHelper.AcceptedImageTransferSyntaxes);
            }
            
            SendAssociationAccept(association);
        }

        public void OnReceiveAssociationReleaseRequest()
        {
            SendAssociationReleaseResponse();
        }
        
        public DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request)
        {
            return new DicomCEchoResponse(request, DicomStatus.Success);
        }

        public IEnumerable<DicomCFindResponse> OnCFindRequest(DicomCFindRequest request)
        {
            var log = new StringBuilder();
            IDicomDatasetWalker walkerTexasRanger = new DicomDatasetDumper(log);
            new DicomDatasetWalker(request.Dataset).Walk(walkerTexasRanger);
            
            //var description = string.Format("C-FIND -> {0}", request.Dataset.Get<DicomSequence>(DicomTag.ScheduledProcedureStepSequence).First().Get(DicomTag.ScheduledProcedureStepStartDate, "NONE"));
            
            List<DicomCFindResponse> responses = new List<DicomCFindResponse>();
            var results = m_WorklistRepository.WorklistItems;
            
            foreach (DicomDataset result in results)
            {
                //result.Add(DicomTag.RetrieveAETitle, "meeeeh");
                var response = new DicomCFindResponse(request, DicomStatus.Pending) { Dataset = result };
                responses.Add(response);
            }
            responses.Add(new DicomCFindResponse(request, DicomStatus.Success));
            return responses;
        }
        
        public DicomNCreateResponse OnNCreateRequest(DicomNCreateRequest request)
        {
            UpdateMppsStatusInLocalCollections(request);

            var resp = new DicomNCreateResponse(request, DicomStatus.Success);
            return resp;
        }
        
        public DicomNSetResponse OnNSetRequest(DicomNSetRequest request)
        {
            UpdateMppsStatusInLocalCollections(request);

            var resp = new DicomNSetResponse(request, DicomStatus.Success);
            return resp;
        }

        private void UpdateMppsStatusInLocalCollections(DicomRequest request)
        {
            var description = string.Format("{0} -> {1}", request.Dataset.Get<string>(DicomTag.PatientID), request.Dataset.Get<string>(DicomTag.PerformedProcedureStepStatus));
            
            var patientId = request.Dataset.Get<string>(DicomTag.PatientID);
            var status = request.Dataset.Get<string>(DicomTag.PerformedProcedureStepStatus);

            var item = m_WorklistRepository.WorklistItems.Single(w => w.Get<string>(DicomTag.PatientID).Equals(patientId));

            item.AddOrUpdate(DicomTag.PerformedProcedureStepStatus, status);

            SetMppsStatusFromItem(item);
        }

        private void SetMppsStatusFromItem(DicomDataset item)
        {
            //TODO: modify the real list (Configuration.WorklistItems), not the readonly dumped list
            m_WorklistRepository.GetReadableWorklistItems().Single(o => o["Id"].Equals(item.Get<string>(DicomTag.PatientID)))["Status"] =
                item.Get<string>(DicomTag.PerformedProcedureStepStatus);
        }

        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
        }

        public DicomNActionResponse OnNActionRequest(DicomNActionRequest request)
        {
            return null;
        }

        public DicomNDeleteResponse OnNDeleteRequest(DicomNDeleteRequest request)
        {
            return null;
        }

        public DicomNEventReportResponse OnNEventReportRequest(DicomNEventReportRequest request)
        {
            return null;
        }

        public DicomNGetResponse OnNGetRequest(DicomNGetRequest request)
        {
            return null;
        }

        public void OnConnectionClosed(Exception exception)
        {
        }
    }
}

using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using Sogeti.Academy.Application.Presentations.Models;

namespace Sogeti.Academy.Persistence.Test.Presentations
{
    public class PresentationDocument : Document
    {
        private readonly Presentation _presentation;

        public override string Id
        {
            get { return _presentation.Id; }
            set { _presentation.Id = value; }
        }

        public PresentationDocument(Presentation presentation)
        {
            _presentation = presentation;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(_presentation);
        }
    }
}
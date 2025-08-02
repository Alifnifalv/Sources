using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    public class NewsletterSubscriptionViewModel : BaseMasterViewModel
    {
        public string emailID { get; set; }
        public long cultureID { get; set; }

        public static NewsletterSubscriptionDTO ToDTO (NewsletterSubscriptionViewModel vm)
        {
            Mapper<NewsletterSubscriptionViewModel, NewsletterSubscriptionDTO>.CreateMap();
            var mapper = Mapper<NewsletterSubscriptionViewModel, NewsletterSubscriptionDTO>.Map(vm);

            return mapper;

        }
    }
}

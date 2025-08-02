using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Domain.Repository
{
    public class DesginerRepository
    {
        public DesignerOverviewDTO GetDesignerDetail(int designerIID)
        {
            List<DesignerOverviewDTO> listDesignerOverviewDTO = new List<DesignerOverviewDTO>();
            DesignerOverviewDTO designerOverviewDTO = new DesignerOverviewDTO();

            // First
            designerOverviewDTO.DesignerId = 1;
            designerOverviewDTO.DesignerName = "APM Monaco";
            designerOverviewDTO.DesignerDescription = @"<p>APM is a trendy and accessible jewelry brand. APM is all about indulging yourself with a timeless piece or trendy and sophisticated jewelry to create a new look. We propose a new collection every month following the latest trends.</p>
                                                        <p>
                                                        Ariane Prette created the company in 1982, following her passion for jewelry, diamond, precious stones, gold and silver.<br>
                                                        The family continued carrying her legacy through the years as a manufacturer, developing traditional handcrafted quality jewelry.
                                                        </p>
                                                        <p>So when the time came it seemed natural to launch the brand, creating beautiful jewelry and designs keeping high quality at affordable prices.</p>";
            designerOverviewDTO.DesignerImagePath = "https://Eduegates-static.s3.amazonaws.com/media/cache/3f/64/3f649d3c3490f209337386924ed643c1.jpg";

            listDesignerOverviewDTO.Add(designerOverviewDTO);

            // Seconad
            designerOverviewDTO = new DesignerOverviewDTO();
            designerOverviewDTO.DesignerId = 2;
            designerOverviewDTO.DesignerName = "Baan";
            designerOverviewDTO.DesignerDescription = @"<p>Clementine was born in Avignon, France. Her love for travelling sent her throughout the Far East, from Thailand, Indonesia, Singapore, Malaysia, and more. Inspired by her travels and with a background in design, she soon set up her own boutique Baan in 2010, in her hometown.</p>
                                                        <p>Baan, meaning ‘home’ in Thai, provides home & fashion accessories. Items are both creations and trip findings, chosen or made with care for their utility, history, and material. Baan’s atmosphere is very soft and sober as the designer leans to simplicity, serenity and nature.</p>";
            designerOverviewDTO.DesignerImagePath = "https://Eduegates-static.s3.amazonaws.com/media/cache/14/9e/149e8842fd7c56547c98b52796be9a23.jpg";

            listDesignerOverviewDTO.Add(designerOverviewDTO);


            designerOverviewDTO = (from list in listDesignerOverviewDTO
                                   where list.DesignerId == designerIID
                                        select list).FirstOrDefault();

            return designerOverviewDTO;
        }

        
    }
}

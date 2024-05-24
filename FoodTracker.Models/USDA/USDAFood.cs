using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.NewFolder
{
    public class USDAFood
    {
        public int FdcId {get; set;}
        public string Description {get; set;}
        public string DataType {get; set;}
        public string GtinUpc {get; set;}
        public string PublishedDate {get; set;}
        public string BrandOwner {get; set;}
        public string BrandName {get; set;}
        public string Ingredients {get; set;}
        public string MarketCountry {get; set;}
        public string FoodCategory {get; set;}
        public string ModifiedDate {get; set;}
        public string DataSource {get; set;}
        public string PackageWeight {get; set;}
        public string ServingSizeUnit {get; set;}
        public decimal ServingSize {get; set;}
        public string HouseholdServingFullText {get; set;}
        public string ShortDescription {get; set;}
        //tradeChannels {get; set;}
        public string AllHighlightFields {get; set;}
        public decimal Score {get; set;}

        //microbes {get; set;}
        //foodNutrients {get; set;}
        //finalFoodInputFoods {get; set;}
        //foodMeasures {get; set;}
        //foodAttributes {get; set;}
        //foodAttributeTypes {get; set;}
        //foodVersionIds { get; set;}
        public string MaxReactionColor { get; set; }
        public string MaxKnownFodColor { get; set; }
        public int AppFoodId { get; set; }
        public int AppFodmapId { get; set;}
        public int Id { get; set; }
    }
}

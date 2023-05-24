import { Request, Response } from 'express';
import FoodService from '@services/food.service';
import catchAsync from '../utils/catchAsync';

class FoodController {
  private foodService;
  constructor() {
    this.foodService = new FoodService();
  }

  getAllFoods = catchAsync(async (req: Request, res: Response) => {
    const data = await this.foodService.getAllFoods();
    // console.log(data)

    const data2 = {
      totalHits: 1,
      currentPage: 1,
      totalPages: 1,
      pageList: [1],
      foodSearchCriteria: {
        query: '638031612178',
        generalSearchInput: '638031612178',
        pageNumber: 1,
        numberOfResultsPerPage: 50,
        pageSize: 10,
        requireAllWords: false,
      },
      foods: [
        {
          fdcId: 1857905,
          description:
            'SMOKED APPLE & SAGE PLANT-BASED SAUSAGES, SMOKED APPLE & SAGE',
          lowercaseDescription:
            'smoked apple & sage plant-based sausages, smoked apple & sage',
          dataType: 'Branded',
          gtinUpc: '638031612178',
          publishedDate: '2021-07-29',
          brandOwner: 'Greenleaf Foods, SPC',
          brandName: 'FIELD ROAST',
          ingredients:
            'FILTERED WATER, VITAL WHEAT GLUTEN, EXPELLER PRESSED SAFFLOWER OIL, UNSULFURED DRIED APPLES, YUKON GOLD POTATOES, YEAST EXTRACT (YEAST, SALT), ONION POWDER, BARLEY MALT EXTRACT, GARLIC, SPICES, SEA SALT, YEAST, RUBBED SAGE, NATURAL SMOKE FLAVOR.',
          marketCountry: 'United States',
          foodCategory: 'Other Meats',
          modifiedDate: '2021-02-24',
          dataSource: 'LI',
          packageWeight: '12.95 OZ/368 g',
          servingSizeUnit: 'g',
          servingSize: 92.0,
          allHighlightFields: '<b>GTIN/UPC</b>: <em>638031612178</em>',
          score: -539.1026,
          foodNutrients: [
            {
              nutrientId: 1003,
              nutrientName: 'Protein',
              nutrientNumber: '203',
              unitName: 'G',
              derivationCode: 'LCCS',
              derivationDescription:
                'Calculated from value per serving size measure',
              derivationId: 70,
              value: 25.0,
              foodNutrientSourceId: 9,
              foodNutrientSourceCode: '12',
              foodNutrientSourceDescription:
                "Manufacturer's analytical; partial documentation",
              rank: 600,
              indentLevel: 1,
              foodNutrientId: 22718822,
            },
            {
              nutrientId: 1004,
              nutrientName: 'Total lipid (fat)',
              nutrientNumber: '204',
              unitName: 'G',
              derivationCode: 'LCCS',
              derivationDescription:
                'Calculated from value per serving size measure',
              derivationId: 70,
              value: 8.7,
              foodNutrientSourceId: 9,
              foodNutrientSourceCode: '12',
              foodNutrientSourceDescription:
                "Manufacturer's analytical; partial documentation",
              rank: 800,
              indentLevel: 1,
              foodNutrientId: 22718823,
              percentDailyValue: 10,
            },
            {
              nutrientId: 1005,
              nutrientName: 'Carbohydrate, by difference',
              nutrientNumber: '205',
              unitName: 'G',
              derivationCode: 'LCCS',
              derivationDescription:
                'Calculated from value per serving size measure',
              derivationId: 70,
              value: 17.4,
              foodNutrientSourceId: 9,
              foodNutrientSourceCode: '12',
              foodNutrientSourceDescription:
                "Manufacturer's analytical; partial documentation",
              rank: 1110,
              indentLevel: 2,
              foodNutrientId: 22718824,
              percentDailyValue: 6,
            },
            {
              nutrientId: 1008,
              nutrientName: 'Energy',
              nutrientNumber: '208',
              unitName: 'KCAL',
              derivationCode: 'LCCS',
              derivationDescription:
                'Calculated from value per serving size measure',
              derivationId: 70,
              value: 239,
              foodNutrientSourceId: 9,
              foodNutrientSourceCode: '12',
              foodNutrientSourceDescription:
                "Manufacturer's analytical; partial documentation",
              rank: 300,
              indentLevel: 1,
              foodNutrientId: 22718825,
            },
            {
              nutrientId: 2000,
              nutrientName: 'Sugars, total including NLEA',
              nutrientNumber: '269',
              unitName: 'G',
              derivationCode: 'LCCS',
              derivationDescription:
                'Calculated from value per serving size measure',
              derivationId: 70,
              value: 4.35,
              foodNutrientSourceId: 9,
              foodNutrientSourceCode: '12',
              foodNutrientSourceDescription:
                "Manufacturer's analytical; partial documentation",
              rank: 1510,
              indentLevel: 3,
              foodNutrientId: 22718826,
            },
            {
              nutrientId: 1079,
              nutrientName: 'Fiber, total dietary',
              nutrientNumber: '291',
              unitName: 'G',
              derivationCode: 'LCCD',
              derivationDescription:
                'Calculated from a daily value percentage per serving size measure',
              derivationId: 75,
              value: 0.0,
              foodNutrientSourceId: 9,
              foodNutrientSourceCode: '12',
              foodNutrientSourceDescription:
                "Manufacturer's analytical; partial documentation",
              rank: 1200,
              indentLevel: 3,
              foodNutrientId: 22718827,
              percentDailyValue: 0,
            },
            {
              nutrientId: 1087,
              nutrientName: 'Calcium, Ca',
              nutrientNumber: '301',
              unitName: 'MG',
              derivationCode: 'LCCS',
              derivationDescription:
                'Calculated from value per serving size measure',
              derivationId: 70,
              value: 43.0,
              foodNutrientSourceId: 9,
              foodNutrientSourceCode: '12',
              foodNutrientSourceDescription:
                "Manufacturer's analytical; partial documentation",
              rank: 5300,
              indentLevel: 1,
              foodNutrientId: 22718828,
              percentDailyValue: 4,
            },
            {
              nutrientId: 1089,
              nutrientName: 'Iron, Fe',
              nutrientNumber: '303',
              unitName: 'MG',
              derivationCode: 'LCCS',
              derivationDescription:
                'Calculated from value per serving size measure',
              derivationId: 70,
              value: 1.74,
              foodNutrientSourceId: 9,
              foodNutrientSourceCode: '12',
              foodNutrientSourceDescription:
                "Manufacturer's analytical; partial documentation",
              rank: 5400,
              indentLevel: 1,
              foodNutrientId: 22718829,
              percentDailyValue: 8,
            },
            {
              nutrientId: 1092,
              nutrientName: 'Potassium, K',
              nutrientNumber: '306',
              unitName: 'MG',
              derivationCode: 'LCCS',
              derivationDescription:
                'Calculated from value per serving size measure',
              derivationId: 70,
              value: 228,
              foodNutrientSourceId: 9,
              foodNutrientSourceCode: '12',
              foodNutrientSourceDescription:
                "Manufacturer's analytical; partial documentation",
              rank: 5700,
              indentLevel: 1,
              foodNutrientId: 22718830,
              percentDailyValue: 4,
            },
            {
              nutrientId: 1093,
              nutrientName: 'Sodium, Na',
              nutrientNumber: '307',
              unitName: 'MG',
              derivationCode: 'LCCS',
              derivationDescription:
                'Calculated from value per serving size measure',
              derivationId: 70,
              value: 609,
              foodNutrientSourceId: 9,
              foodNutrientSourceCode: '12',
              foodNutrientSourceDescription:
                "Manufacturer's analytical; partial documentation",
              rank: 5800,
              indentLevel: 1,
              foodNutrientId: 22718831,
              percentDailyValue: 24,
            },
            {
              nutrientId: 1110,
              nutrientName: 'Vitamin D (D2 + D3), International Units',
              nutrientNumber: '324',
              unitName: 'IU',
              derivationCode: 'LCCD',
              derivationDescription:
                'Calculated from a daily value percentage per serving size measure',
              derivationId: 75,
              value: 0.0,
              foodNutrientSourceId: 9,
              foodNutrientSourceCode: '12',
              foodNutrientSourceDescription:
                "Manufacturer's analytical; partial documentation",
              rank: 8650,
              indentLevel: 1,
              foodNutrientId: 22718832,
              percentDailyValue: 0,
            },
            {
              nutrientId: 1235,
              nutrientName: 'Sugars, added',
              nutrientNumber: '539',
              unitName: 'G',
              derivationCode: 'LCCD',
              derivationDescription:
                'Calculated from a daily value percentage per serving size measure',
              derivationId: 75,
              value: 0.0,
              foodNutrientSourceId: 9,
              foodNutrientSourceCode: '12',
              foodNutrientSourceDescription:
                "Manufacturer's analytical; partial documentation",
              rank: 1540,
              indentLevel: 0,
              foodNutrientId: 22718833,
              percentDailyValue: 0,
            },
            {
              nutrientId: 1253,
              nutrientName: 'Cholesterol',
              nutrientNumber: '601',
              unitName: 'MG',
              derivationCode: 'LCCD',
              derivationDescription:
                'Calculated from a daily value percentage per serving size measure',
              derivationId: 75,
              value: 0.0,
              foodNutrientSourceId: 9,
              foodNutrientSourceCode: '12',
              foodNutrientSourceDescription:
                "Manufacturer's analytical; partial documentation",
              rank: 15700,
              indentLevel: 1,
              foodNutrientId: 22718834,
              percentDailyValue: 0,
            },
            {
              nutrientId: 1257,
              nutrientName: 'Fatty acids, total trans',
              nutrientNumber: '605',
              unitName: 'G',
              derivationCode: 'LCCS',
              derivationDescription:
                'Calculated from value per serving size measure',
              derivationId: 70,
              value: 0.0,
              foodNutrientSourceId: 9,
              foodNutrientSourceCode: '12',
              foodNutrientSourceDescription:
                "Manufacturer's analytical; partial documentation",
              rank: 15400,
              indentLevel: 1,
              foodNutrientId: 22718835,
            },
            {
              nutrientId: 1258,
              nutrientName: 'Fatty acids, total saturated',
              nutrientNumber: '606',
              unitName: 'G',
              derivationCode: 'LCCS',
              derivationDescription:
                'Calculated from value per serving size measure',
              derivationId: 70,
              value: 0.54,
              foodNutrientSourceId: 9,
              foodNutrientSourceCode: '12',
              foodNutrientSourceDescription:
                "Manufacturer's analytical; partial documentation",
              rank: 9700,
              indentLevel: 1,
              foodNutrientId: 22718836,
              percentDailyValue: 3,
            },
          ],
          finalFoodInputFoods: [],
          foodMeasures: [],
          foodAttributes: [],
          foodAttributeTypes: [],
          foodVersionIds: [],
        },
      ],
      aggregations: { dataType: { Branded: 1 }, nutrients: {} },
    };

    res.status(200).json({
      status: 'success',
      data,
    });
  });

  addFoods = catchAsync(async (req: Request, res: Response) => {
    let data;
    if (req.body.data) {
      data = await this.foodService.addFoods(req.body.data);
    } else {
      data = await this.foodService.addFoods([req.body]);
    }

    res.status(200).json({
      status: 'success',
      data,
    });
  });

  deleteFood = catchAsync(async (req: Request, res: Response) => {
    await this.foodService.deleteFood(req.params.id)

    res.status(200).json({
      status: 'success',
    });
  });
}

export default new FoodController();

export interface FoodDBObject {
  id?: number;
  name: string;
  fodmapId: number;
  vegetarian: boolean;
  vegan: boolean;
  glutenFree: boolean;
}

export interface FoodUpdateObject {
  modifiedBy: string;
  name?: string;
  fodmapId?: number;
  vegetarian?: boolean;
  vegan?: boolean;
  glutenFree?: boolean;
}

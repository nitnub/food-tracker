export interface FoodDbResponse {
  id: number;
  name: string;
  fodmapId: number;
  vegetarian: boolean;
  vegan: boolean;
  glutenFree: boolean;
}

// export interface Food extends FoodDbRespones {
//   reaction: Reaction
// }
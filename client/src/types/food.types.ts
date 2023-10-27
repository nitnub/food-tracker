import { ReactionEntry } from "./dbTypes";

export interface FoodDbResponse {
  id: number;
  name: string;
  fodmapId: number;
  vegetarian: boolean;
  vegan: boolean;
  glutenFree: boolean;
  reactions: ReactionEntry[]
}

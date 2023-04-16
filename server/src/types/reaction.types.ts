export interface ReactionItem {
  displayName: string;
  id: number;
}

export interface Reaction {
  id?: number;
  user: ReactionItem;
  food: ReactionItem;
  reactionType: ReactionItem;
  severity: ReactionItem;
  active: boolean;
  identifiedOn?: string;
  subsidedOn?: string;
  lastModifiedOn?: string;
}

export interface ReactionDbEntry {
  id?: number;
  userId: number;
  // foodId: number
  elementId: number;
  foodGroupingId: number;
  reactionTypeId: number;
  severityId: number;
  active?: boolean;
  identifiedOn?: string;
  subsidedOn?: string;
  lastModifiedOn?: string;
  deletedOn?: string;
}

export interface FoodResponse {
  id: number;
  reactionScope: string;
  name: string;
  vegetarian: boolean;
  vegan: boolean;
  glutenFree: boolean;
  fodMap: FodmapResponse
};

export interface FodmapResponse {
  id: number;
  category: string;
  name: string;
  freeUse: boolean;
  oligos: boolean;
  fructose: boolean;
  polyols: boolean;
  lactose: boolean;
  color: string;
  maxIntake: string;
}

export interface ReactionDetails { 
  category: string; 
  type: string; 
  severity: string 
};

export interface ReactionComplete {
  reactionId: number;
  active: boolean;
  subsidedOn: string;
  modifiedOn: string;
  identifiedOn: string;
  deletedOn: string;
  food: FoodResponse;
  reaction: ReactionDetails
}

export interface ReactionDbResponse {
  id: number;
  userId: number;
  active: boolean;
  subsidedOn: string;
  modifiedOn: string;
  identifiedOn: string;
  deletedOn: string;
  reactionType: string;
  reactionCategory: string;
  reactionScope: string;
  foodId: number;
  foodName: string;
  vegetarian: boolean;
  vegan: boolean;
  glutenFree: boolean;
  reactionSeverity: string;
  fodId: number;
  fodCategory: string;
  fodName: string;
  fodFreeUse: boolean;
  fodOligos: boolean;
  fodFructose: boolean;
  fodPolyols: boolean;
  fodLactose: boolean;
  fodColor: string;
  maxIntake: string;
  maxIntakeTest: string;
}

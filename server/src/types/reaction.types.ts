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
  severityId: number;
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
  id: number;
  category: string; 
  type: string; 
  severityId: number 
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
  reactionTypeName: string;
  reactionTypeId: number;
  reactionCategory: string;
  reactionScope: string;
  foodGroupingId: number;
  foodId: number;
  foodName: string;
  vegetarian: boolean;
  vegan: boolean;
  glutenFree: boolean;
  severityName: string;
  severityId: number;
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
}

export interface UnformattedReactionDbResponse {
  json_build_object: ReactionDbResponse[]
}
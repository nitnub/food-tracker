export interface Severity {
  id: number;
  name: string;
}

export interface ReactionType {
  id: number;
  name: string;
}

export interface Category {
  id: number;
  name: string;
  reactionTypes?: ReactionType[];
}

export interface ReactionOptionProps {
  severities: Severity[];
  categories: Category[];
}

export interface ReactionEntry {
  // typeId: number;
  userId: number;
  elementId: number;
  reactionType: number;
  severity: number;
  foodGroupingId?: number;
  active?: boolean;
}
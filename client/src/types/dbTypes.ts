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

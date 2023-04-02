export interface ReactionItem {
  displayName: string
  id: number
}


export interface Reaction {
  id?: number
  user: ReactionItem
  food: ReactionItem
  reactionType: ReactionItem
  severity: ReactionItem
  active: boolean
  identifiedOn?: string
  subsidedOn?: string
  lastModifiedOn?: string
}

export interface ReactionDbEntry {
  id?: number
  userId: number
  foodId: number
  reactionTypeId: number
  severityId: number
  active?: boolean
  identifiedOn?: string
  subsidedOn?: string
  lastModifiedOn?: string
  deletedOn?: string
}
// export interface Reaction {
//   id?: number
//   user: number
//   food: number
//   reactionType: number
//   severity: number
//   active: boolean
//   identifiedOn?: string
//   subsidedOn?: string
//   lastModifiedOn?: string
// }
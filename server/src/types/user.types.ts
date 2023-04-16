interface UserDbEntry {
  id?: number;
  globalUserId: string;
  email: string;
  admin: boolean;
  avatar: string;
  active: boolean;
  createdOn?: string;
  lastModifiedOn?: string;
  deletedOn?: string;
}

interface UserDbUpdateEntry {
  
  email: string;
  admin: boolean;
  avatar: string;
  active: boolean;
  modifiedBy: string;
}

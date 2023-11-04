interface User {
  id: number;
  email: string;
  admin: boolean;
  avatar: string;
  active: boolean;
}

interface UserDbEntry extends User {
  globalUserId?: string;
  createdOn?: string;
  lastModifiedBy?: string;
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

interface NewUserEntry {
  globalUserId: string;
  email: string;
  admin: true;
  avatar: string;
  active?: string;
}

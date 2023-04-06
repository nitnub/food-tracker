interface UserDbEntry {
  id?: number;
  globalUserId: string;
  email: string;
  admin: boolean;
  avatar: string
  active: boolean
  createdOn?: string
  lastModifiedOn?: string
  deletedOn?: string
}



// {"globalUserId": "AWEF3r23r",
//   "email": "aTestEmail@gmail.com",
//   "admin": true,
//   "avatar": "mysite.com/avatar/link"
// }
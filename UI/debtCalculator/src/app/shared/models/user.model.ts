
export class UserModel {
  id: number;
  name: string;
  email: string;
  idProfile: number;
}

export class AddUserModel {
  name: string;
  email: string;
  cpf: string;
}

export class EditUserModel {
  id: number;
  name: string;
  email: string;
}

export class UserPaginationModel {
  data: UserModel[];
  total: number;
}

export class UserLoginModel {
  id: number;
  name: string;
  email: string;
  idProfile: number;
}


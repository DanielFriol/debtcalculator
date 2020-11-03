import { UserLoginModel } from './user.model';

export class StorageDataModel {
    token: string;
    expires: Date;
    authTime: Date;
    user: UserLoginModel;
    env: string;
}

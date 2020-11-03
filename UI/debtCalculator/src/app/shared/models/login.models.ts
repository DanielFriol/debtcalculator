
export class LoginModel {
    email: string;
    password: string;
}

export class LoginResult {
    access_token: string;
    environment: string;
}

export class SendVerificationCodeModel {
    email: string;
}

export class ChangePasswordModel {
    email: string;
    verificationCode?: string;
    oldPassword?: string;
    newPassword: string;
    grantType?: string;
}

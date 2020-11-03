import { AbstractControl } from '@angular/forms';

export class PasswordValidator {

    static matchPassword(AC: AbstractControl) {
        let password = AC.get('password').value;
        let confirmPassword = AC.get('confirmPassword').value;
        if (password != confirmPassword) {
            AC.get('confirmPassword').setErrors({ MatchPassword: true })
        }
        return null;
    }

}

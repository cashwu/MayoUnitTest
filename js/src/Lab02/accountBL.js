import {AccountDao} from "./accountDao";
import {Cryptography} from "./cryptography";


export class AccountBL {
    
    login(account, password) {
        
        let accountDao = new AccountDao();
        let member = accountDao.getMemberForLogin(account);
        let encryptedPassword = new Cryptography().cashSha(password);
        let isValid = member.password === encryptedPassword;
        
        if (isValid) {
            return true;
        } else {
            return false;
        }
    }
}
import {AccountDao} from "./accountDao";
import {Cryptography} from "./cryptography";

export class AccountBL {
    
    login(account, password) {
        let member = this.getMember(account);
        let encryptedPassword = this.getShaPassword(password);
        let isValid = member.password === encryptedPassword;
        
        if (isValid) {
            return true;
        } else {
            this.setLoginFailedCount(account);

            this.send(`${account} login failed`)

            return false;
        }
    }

    setLoginFailedCount(account) {
        let accountDao = new AccountDao();
        accountDao.setLoginFailedCount(account);
    }

    send(message) {
       console.log(message);
    }

    getShaPassword(password) {
        let encryptedPassword = new Cryptography().cashSha(password);
        return encryptedPassword;
    }

    getMember(account) {
        let accountDao = new AccountDao();
        let member = accountDao.getMemberForLogin(account);
        return member;
    }
}
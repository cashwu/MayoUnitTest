export class AccountDao {

    getMemberForLogin(account) {
        
        const profile = {
            "cash" : {
                "password" : "1234"
            }
        };
        
        return profile[account];
    }

    setLoginFailedCount(account) {
        
    }

    getLoginFailedCount() {
        return 0;
    }
}
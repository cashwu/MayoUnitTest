import {AccountBL} from "../src/Lab02/accountBL";

describe("accountBL", () => {

    let accountBL = new AccountBL();
    let fake_getMember;
    let fake_getShaPassword;
    let fake_send;
    let fake_setLoginFailedCount;
    let fake_getLoginFailedCount;

    beforeEach(() => {
        accountBL = new AccountBL();
        fake_getMember = jest.fn();
        accountBL.getMember = fake_getMember;
        fake_getShaPassword = jest.fn();
        accountBL.getShaPassword = fake_getShaPassword;
        fake_send = jest.fn();
        accountBL.send = fake_send;
        fake_setLoginFailedCount = jest.fn();
        accountBL.setLoginFailedCount = fake_setLoginFailedCount;
        fake_getLoginFailedCount = jest.fn();
        accountBL.getLoginFailedCount = fake_getLoginFailedCount;
    })

    it("login is valid", () => {
        givenMember({
            "password": "sha-1234"
        });

        givenShaPassword("sha-1234");

        loginShouldBeValid("cash", "12345678");
        shouldNotLog();
        shouldNotSetFailedCount();
    })

    it("login is invalid", () => {
        givenMember({
            "password": "sha-1234"
        });

        // 注意這裡也要改 !!
        givenShaPassword("sha-5678");
        loginShouldInvalid("cash", "wrong password");
    })

    it("login invalid should log and set failed count", () => {
        givenLoginInvalid();
       
        // expect(fake_send.mock.calls[0][0]).toBe("cash login failed");
        shouldLog("cash", "login failed");
        
        shouldSetFailedCount("cash");
    })

    it("login failed 5 times should throw exception", () => {

        givenMember({
            "password": "sha-1234"
        });

        givenShaPassword("sha-5678");
        
        givenLoginFailedCount(4);
        
        let func = () => accountBL.login("cash", "wrong password");
        expect(func).toThrow("cash login failed more than 5 times");
    })

    function givenLoginFailedCount(failedCount) {
        fake_getLoginFailedCount.mockReturnValueOnce(failedCount);
    }

    function shouldNotSetFailedCount() {
        expect(fake_setLoginFailedCount.mock.calls.length).toBe(0);
    }

    function shouldSetFailedCount(account) {
        expect(fake_setLoginFailedCount.mock.calls[0][0]).toBe(account);
    }

    function shouldNotLog() {
        expect(fake_send.mock.calls.length).toBe(0);
    }

    function shouldLog(account, status) {
        expect(fake_send.mock.calls[0][0]).toEqual(
            expect.stringContaining(account) && expect.stringContaining(status)
        )
    }

    function givenMember(member) {
        fake_getMember.mockReturnValueOnce(member);
    }

    function givenShaPassword(shaPassword) {
        fake_getShaPassword.mockReturnValueOnce(shaPassword);
    }

    function loginShouldBeValid(account, password) {
        let isValid = accountBL.login(account, password);
        expect(isValid).toBe(true)
    }

    function loginShouldInvalid(account, password) {
        let isValid = accountBL.login(account, password);
        expect(isValid).toBe(false)
    }

    function givenLoginInvalid() {
        givenMember({
            "password": "sha-1234"
        });

        // 注意這裡也要改 !!
        givenShaPassword("sha-5678");

        accountBL.login("cash", "wrong password");
    }
})
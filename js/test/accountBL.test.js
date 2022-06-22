import {AccountBL} from "../src/Lab02/accountBL";

describe("accountBL", () => {

    let accountBL = new AccountBL();
    let fake_getMember;
    let fake_getShaPassword;
    
    beforeEach(() => {
        accountBL = new AccountBL();
        fake_getMember = jest.fn();
        accountBL.getMember = fake_getMember;
        fake_getShaPassword = jest.fn();
        accountBL.getShaPassword = fake_getShaPassword;
    })

    it("login is valid", () => {
        givenMember({
            "password": "sha-1234"
        });
        
        givenShaPassword("sha-1234");
        
        loginShouldBeValid("cash", "12345678");
    })

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
})
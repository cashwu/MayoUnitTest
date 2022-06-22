import {DateUtility} from "../src/Lab01/dateUtility";

describe("date utility", () => {

    it("today is payday", () => {
        
        let dateUtility = new DateUtility();
        let fake_getToday = jest.fn();
        dateUtility.getToday = fake_getToday;
        fake_getToday.mockReturnValueOnce(new Date(2020, 6, 5));

        expect(dateUtility.isPayday()).toBe(true);
    })

});


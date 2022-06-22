import {DateUtility} from "../src/Lab01/dateUtility";

describe("date utility", () => {
    let dateUtility = new DateUtility();
    let fake_getToday = jest.fn();

    beforeEach(()=> {
        dateUtility = new DateUtility();
        fake_getToday = jest.fn();
        dateUtility.getToday = fake_getToday;
    })

    it("today is payday", () => {
        givenToday(5);
        todayShouldBePayday();
    })

    it("today is not payday", () => {
        givenToday(10);
        todayShouldBeNotPayday();
    })
    
    it("today is payday when 20", () => {
        givenToday(20);
        todayShouldBePayday();
    })

    function givenToday(date) {
        fake_getToday.mockReturnValueOnce(new Date(2022, 6, date));
    }

    function todayShouldBePayday() {
        expect(dateUtility.isPayday()).toBe(true);
    }

    function todayShouldBeNotPayday() {
        expect(dateUtility.isPayday()).toBe(false);
    }

});


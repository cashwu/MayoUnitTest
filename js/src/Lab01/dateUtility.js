export class DateUtility {

    isPayday() {
        const date = this.getToday();

        if (date.getDate() === 5) {
            return true;
        }

        return false;
    }

    getToday() {
        const date = new Date();
        return date;
    }
}

export class DateUtility {

    isPayday() {
        const date = new Date();

        if (date.getDate() === 5) {
            return true;
        }

        return false;
    }
}

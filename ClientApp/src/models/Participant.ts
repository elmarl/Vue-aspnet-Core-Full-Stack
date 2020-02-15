export class Participant {
    constructor(
        public id: number,
        public firstname: string,
        public familyname: string = '',
        public idcode: string,
        public numparticipants: number = 0,
        public paymentmethod: string,
        public details: string,
        public ptype: string,
        public fkey: number,
    ) { }
}

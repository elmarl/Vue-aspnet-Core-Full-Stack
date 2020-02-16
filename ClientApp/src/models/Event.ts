export class Event {
    constructor(
        public eventid: number = 0,
        public eventName: string,
        public eventDate: string,
        public location: string,
        public details: string,
    ) { }
}

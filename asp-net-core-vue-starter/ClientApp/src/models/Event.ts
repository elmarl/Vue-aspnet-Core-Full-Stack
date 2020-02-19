export class Event {
    constructor(
        public eventid: number = 0,
        public eventName: string | null,
        public eventDate: string | null,
        public location: string | null,
        public details: string | null,
    ) { }
}

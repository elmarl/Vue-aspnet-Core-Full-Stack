export class Event {
    constructor(
        public id: number,
        public name: string,
        public date: Date,
        public location: string,
        public details: string,
    ) { }
}

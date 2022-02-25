import { Ref } from "vue";
import Question from "./Question";
export default class Quiz
{
    id: number;
    title: Ref | string;
    questions: Question[];

    constructor(id: number, title: Ref | string, questions: Question[])
    {
        this.id = id;
        this.title = title;
        this.questions = questions;
    }

}
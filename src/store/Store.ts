import { defineStore, storeToRefs } from 'pinia';
import Quiz from '@/classes/Quiz';

import { apiURL } from '@/constants/constants';
import { Ref, ref } from 'vue';
import Question from '@/classes/Question';

export const useStore = defineStore('Store',
{
    state: () =>
    (
        {
            quizList: {} as {[id: number]: Quiz},
            
            loginModal: ref(true) as Ref,
            quizModal: ref(true) as Ref,
            questionModal: ref(false) as Ref,
            newQuizModal: ref(false) as Ref,
            newQuestionModal: ref(false) as Ref,

            quizToLoad: ref(0) as Ref,
            questionToLoad: ref(0) as Ref,
            numberOfQuizzes: ref() as Ref,
            
            isLoggedIn: !!localStorage.getItem("loggedin"),

            userRole: ref('') as Ref,
            userPersist: localStorage.setItem('userPersist', ''),
            username: ref('') as Ref,
            password: ref('') as Ref,

            showAnswer: ref(false)as Ref,

            //Initialising quesiton and answer results from new quiz modal
            title: "",
            newQuizID: 0,
            newQuestions: undefined as Question | any,
            newAnswers: [""],
            newQuestionList: [] as any,

            isEditingQuestion: ref(false) as Ref,

        }
    ),

    actions:
    {
        login()
        {
            this.isLoggedIn = true; 
            localStorage.setItem("loggedin", "value");
            this.loginModal = false;

            localStorage.setItem('userPersist', this.userRole.toString());

        },

        logout()
        {
            this.isLoggedIn = false;
            this.loginModal = !this.loginModal;
            this.userRole = '';
            this.username = '';
            this.password = '';

            localStorage.clear();
        },

        getQuizzes(): void
        {
            fetch(apiURL,
                {
                    headers: { Accept: 'application/json' },
                })
            .then(response => {
                return response.json()
            })
            .then(data =>
            {
                let newQuiz: Quiz;
                data.forEach((quiz: any) => 
                {
                    newQuiz = new Quiz(quiz.id, quiz.title, quiz.questions);
                    this.quizList[newQuiz.id] = newQuiz;
                })
            })
        },

        postQuiz(quiz: Quiz): void
        {
            console.log(JSON.stringify(quiz).toString())
            
            fetch(apiURL,
                {
                    method: 'POST',
                    headers: 
                    {
                        "Content-Type": "application/json",
                        "Accept": "application/json",
                    },
                    body: JSON.stringify(quiz).toString()
                })
            this.getQuizzes();
        },

        putQuiz(quiz: Quiz):void 
        {
            
            fetch(apiURL + quiz.id,
                {
                    method: 'PUT',
                    headers: 
                    {
                        "Content-Type": "application/json",
                        "Accept": "application/json",
                    },
                    body: JSON.stringify(quiz).toString()
                })
            this.getQuizzes();
        },

        deleteQuiz(id: number):void
        {
            fetch(apiURL + id,
            {
                method: 'DELETE'
            })
        },

        loadQuestions(quiz: number): void
        {
            this.questionModal = !this.questionModal;
            this.quizToLoad = quiz;
        },

        showAnswers(): void
        {
            this.showAnswer = !this.showAnswer;
        },

        addQuestions(question: Question): void
        {
            this.newQuestions = question;
        },
        
        addAnswers(answers: any): void
        {
            this.newAnswers = answers;
        },

        indexToChar(index: number): string
        {
            let char: string;
            if (index == 0)
            {
                char = 'A';
            }
            else if (index == 1)
            {
                char = 'B';
            }
            else if (index == 2)
            {
                char = 'C';
            }
            else if (index == 3)
            {
                char = 'D';
            }
            else
            {
                char = 'E';
            }
            return char;
        },

        clear(): void
        {
            //Clearing uestion and answer results from new quiz modal
            this.newQuestions = []
            this.newAnswers = []
            this.newQuestionList= []
            this.title = ""
            this.newQuizID = 0
        },





    }
})
<template>
    <div class="absolute flex justify-center items-center bg-black bg-opacity-60 w-screen h-screen z-1" >
        <div class="bg-etonblue rounded-lg px-14 py-8 shadow-lg container w-1/2 overflow-y-auto scrollbar scrollbar-thumb-custom scrollbar-track-custom-light">
            <div class="font-bold text-black text-center text-6xl"> Add New Question</div>
            
            <div class="m-4" >
                <div class="newQuestion" >
                    <div class="flex m-2" >
                        <label class="basis-1/4 font-bold flex-auto  text-3xl" for="question">
                            Enter the Question:
                        </label>
                        <input type="text" required id="question" name="question"  class="w-2/3 p-1 rounded-lg" v-model="questionTitle"> 
                    </div>
                    
                    <label class="basis-1/4 font-bold flex-auto m-2 text-3xl" for="answers">
                        Enter the Answers:
                    </label>
                    <div class="grid m-2"> 
                        <div class="flex" v-for="(input, index) in answerList" :key="index" >
                            <input type="text" id="answer" name="answer" class="w-full p-1 mb-2 rounded-lg" v-model="input.answer">
                            
                            <img src="@/assets/remove.svg" class="p-1 cursor-pointer" @click="removeAnswer(index, answerList)" v-show="answerList.length > 3 && answerList.length < 6">
                            
                            <img src="@/assets/add.svg" class="p-1 cursor-pointer" @click="addAnswer(input, answerList)" v-show="answerList.length < 5"> 
                        </div>
                    </div>
                </div>

                <div 
                class="bg-green-500 font-bold hover:bg-white text-black items-center text-center text-4xl rounded-lg m-2 p-2 cursor-pointer" 
                @click="submitQuestion()">
                    Save  
                </div>
                
                <div 
                class="bg-red-500 font-bold hover:bg-white text-black items-center text-center text-4xl rounded-lg m-2 p-2 cursor-pointer" 
                @click="closeModal">
                    Cancel
                </div>
            </div>
        </div>
    </div>  
</template>

<script lang="ts">
import { defineComponent, ref, Ref } from 'vue';
import { useStore } from '@/store/Store';
import Question from '@/classes/Question';


export default defineComponent({
  name: 'NewQuestion',
  components: {
    
  },


  setup()
  {
    const store = useStore();

    const questionTitle: Ref = ref("");
    let question: Question;
    let answers: string[];

    const answerList = ref(
        [
            {
                answer:"" as string
            },
            {
                answer:"" as string
            },
            {
                answer:"" as string
            }
        ]);
    
    function closeModal(): void
    {
        store.newQuestionModal = !store.newQuestionModal;
        
    }

    function addAnswer(value: any, field: any): void
    {
        field.push(
        {
            answer:""
        });
        // console.log(answerList.value)
    }

    function removeAnswer(index: any, field: any): void
    {

        field.splice(index, 1);
    }

    function submitQuestion(): void
    {
        answers = answerList.value.map(({answer}) => answer );
        question = new Question(questionTitle.value, answers);
        // console.log(answers)
        // console.log(question)

        if ( question.question == "")
        {
            window.alert('Please Enter a Question')
        }
        else if ( question.answers[0] == "" || question.answers[1] == "" || question.answers[2] == "" )
        {
            window.alert('Please enter at least 3 answers')
        }
        else
        {
            store.addQuestions(question);
            if (!store.isEditingQuestion)
            {
                store.newQuestionList.push(store.newQuestions)
            }
            else if (store.isEditingQuestion)
            {
                // store.quizList[store.quizToLoad].questions.push(store.newQuestions)
                store.quizList[store.quizToLoad].questions.splice(1, 0, store.newQuestions)
                // console.log(store.newQuestions)
            }
            closeModal();
        }
    }


    return {
        store,

        questionTitle, 

        answerList,

        closeModal,
        addAnswer,
        removeAnswer,
        submitQuestion,
    }
  },
    
});
</script>

<style lang="scss">
.background 
{
  background-image: url(../assets/zig-zag.svg);
}
</style>
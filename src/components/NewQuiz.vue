<template>

    <NewQuestion v-if="store.newQuestionModal" />

    <div class="absolute flex justify-center items-center bg-black bg-opacity-60 w-screen h-screen z-2" >
        <div class="bg-etonblue rounded-lg px-14 py-8 shadow-lg container h-5/6 w-1/2 overflow-y-auto scrollbar scrollbar-thumb-custom scrollbar-track-custom-light">
            <div class="font-bold text-black text-center text-6xl"> Create/Update a Quiz</div>
            
            <div class="m-4" v-if="!store.isEditingQuestion">
                <form @submit="submit"> 
                    <div class="flex m-2">
                        <label class="basis-1/4 font-bold flex-auto text-3xl" for="title">
                            Quiz Title:
                        </label>
                        <input type="text" required id="title" name="title" class="w-2/3 p-1 rounded-lg" v-model="title"> 
                    </div>
                    <div class="font-bold p-2" v-for="(title, index) in store.newQuestionList" :key="index">
                        {{index + 1}}. {{title.question}}
                            <div v-for="(answer, index) in title.answers" :key="index">
                                {{store.indexToChar(index)}}) {{answer}}
                            </div>
                            <div 
                            class="bg-red-500 font-bold hover:bg-white text-black items-center text-center text-4xl rounded-lg m-2 p-2 cursor-pointer" 
                            @click="removeQuestion(index)">
                                Delete Question
                            </div>
                    </div>
                </form> 

                <div 
                class="bg-blue-400 font-bold hover:bg-white text-black items-center text-center text-4xl rounded-lg m-2 p-2 cursor-pointer" 
                @click="addQuestion()">
                    Add Question
                </div>

                <div 
                class="bg-green-500 font-bold hover:bg-white text-black items-center text-center text-4xl rounded-lg m-2 p-2 cursor-pointer" 
                @click="submit"> 
                   Submit
                </div>
                
                <div 
                class="bg-red-500 font-bold hover:bg-white text-black items-center text-center text-4xl rounded-lg m-2 p-2 cursor-pointer" 
                @click="closeModal">
                    Cancel
                </div>
            </div>

            <div class="m-4" v-if="store.isEditingQuestion">
                <form @submit.prevent="submit()"> 

                    <div class="flex m-2">
                        <label class="basis-1/4 font-bold flex-auto text-3xl" for="title">
                            Quiz Title:
                        </label>
                        <input type="text" required id="title" name="title" class="w-2/3 p-1 rounded-lg" 
                        v-model="store.quizList[store.quizToLoad].title" 
                        :placeholder="store.quizList[store.quizToLoad].title" > 
                    </div>

                    <div class="font-bold p-2" v-for="(questionItem, index) in store.quizList[store.quizToLoad].questions" :key="index">
                        
                        Question:
                        <input class="rounded-lg m-2 p-1 w-full" type="text" :placeholder="index + '.' + questionItem.question" v-model="questionItem.question">

                        Answers:
                        <div v-for="(answer, index) in questionItem.answers" :key="index">
                            <input class="rounded-lg m-2 p-1 w-full" type="text" :placeholder="store.indexToChar(index) + ') ' + answer" v-model="questionItem.answers[index]">
                        </div>

                        <div 
                        class="bg-red-500 font-bold hover:bg-white text-black items-center text-center text-4xl rounded-lg m-2 p-2 cursor-pointer" 
                        @click="removeQuestion(index)">
                            Delete Question
                        </div>

                        <div 
                        class="bg-blue-400 font-bold hover:bg-white text-black items-center text-center text-4xl rounded-lg m-2 p-2 cursor-pointer" 
                        @click="addQuestion()">
                            Add Question
                        </div>
                    </div>
                </form> 

                <!-- <div 
                class="bg-blue-400 font-bold hover:bg-white text-black items-center text-center text-4xl rounded-lg m-2 p-2 cursor-pointer" 
                @click="addQuestion()">
                    Add Question
                </div> -->

                <div 
                class="bg-green-500 font-bold hover:bg-white text-black items-center text-center text-4xl rounded-lg m-2 p-2 cursor-pointer" 
                @click="submit"> 
                   Submit
                </div>
                
                <div 
                class="bg-red-500 font-bold hover:bg-white text-black items-center text-center text-4xl rounded-lg m-2 p-2 cursor-pointer" 
                @click="closeModal(); ">
                    Cancel
                </div>
            </div>
        </div>
    </div>  
</template>

<script lang="ts">
import { defineComponent, ref, Ref } from 'vue';
import { useStore } from '@/store/Store';
import Quiz from '@/classes/Quiz';
import NewQuestion from '@/components/NewQuestion.vue';
// import Question from '@/classes/Question';


export default defineComponent({
  name: 'NewQuiz',
  components: {
    NewQuestion,
  },
  

  setup()
  {

    const store = useStore();

    let newQuiz: Quiz;
    let updatedQuiz: Quiz;
    let newQuizId: number = Math.floor((Math.random() * 100000) + 1);
    let title: Ref = ref("");

    function closeModal(): void
    {
        store.newQuizModal = !store.newQuizModal;
        
        store.title = "";
        store.clear();
        // window.location.reload();
        store.getQuizzes();
    }
    
    function addQuestion(): void
    {
        store.newQuestionModal = !store.newQuestionModal;
    }
    
    function submit(): void
    {
        if (title.value == "")
        {
            if (!store.isEditingQuestion)
            {
                window.alert("Please enter a quiz title.");
            }
            else if (store.isEditingQuestion)
            {
                title.value = store.quizList[store.quizToLoad].title;
            }
        }
        else if (store.newQuestionList.length == 0)
        {
            if (!store.isEditingQuestion)
            {
                window.alert("Please add some questions");
            }
            else if (store.isEditingQuestion)
            {
                store.newQuestionList = store.quizList[store.quizToLoad].questions;
                // console.log(store.quizList[store.quizToLoad].questions);
                
            }
        }
            if (!store.isEditingQuestion)
            {
                newQuiz = new Quiz(newQuizId,title.value, store.newQuestionList);
                store.postQuiz(newQuiz);
                store.getQuizzes();
            }
            else if (store.isEditingQuestion)
            {
                updatedQuiz = new Quiz(store.quizList[store.quizToLoad].id, store.quizList[store.quizToLoad].title, store.quizList[store.quizToLoad].questions);
                store.putQuiz(updatedQuiz);
                store.getQuizzes();
            }
            // console.log(JSON.stringify(updatedQuiz))

            store.clear();
            closeModal();
            
    }

    function removeQuestion(index: number): void
    {
        if (!store.isEditingQuestion)
        {
            store.newQuestionList.splice(index, 1)
        }
        else if (store.isEditingQuestion)
        {
            store.quizList[store.quizToLoad].questions.splice(index, 1)
        }
    }
    


    return {
        store,

        title,
        closeModal,
        addQuestion,
        submit,
        removeQuestion

        
    }
  }
  
});
</script>

<style lang="scss">
.background 
{
  background-image: url(../assets/zig-zag.svg);
}
</style>
<template>
    <div class="absolute flex justify-center items-center bg-black bg-opacity-60 w-screen h-full z-3">

        <div class="bg-etonblue rounded-lg h-5/6 w-5/6 overflow-y-auto p-3 scrollbar scrollbar-thumb-custom scrollbar-track-custom-light">

            <div class="flex justify-between ">
                <div class="font-bold text-black text-6xl pl-5"> 
                    {{store.quizList[store.quizToLoad].title}}
                </div>
                <div class="flex">
                    <div v-show="store.userRole=='View' || store.userRole=='Edit'" 
                    v-if="!store.showAnswer" 
                    class="flex self-end p-3 mr-5 ml-3cursor-pointer bg-green-500 rounded-lg hover:bg-white " 
                    @click="store.showAnswers()"> 
                        <img src="@/assets/eye.svg">
                    </div>

                    <div v-show="store.userRole=='View' || store.userRole=='Edit'" 
                    v-else 
                    class="flex self-end p-3 mr-5 ml-3 cursor-pointer bg-green-500 rounded-lg hover:bg-white " 
                    @click="store.showAnswers()"> 
                        <img src="@/assets/eye-off.svg">
                    </div>

                    <div v-show="store.userRole=='Edit'" 
                    v-if="!editingQuestion" 
                    class="self-end p-3 mr-5 ml-3 cursor-pointer bg-green-500 rounded-lg hover:bg-white " 
                    @click="editQuestion"> 
                        <img src="@/assets/edit.svg">
                    </div>

                    <!-- <div v-show="store.userRole=='Edit'" 
                    v-else 
                    class="flex self-end p-3 mr-5 ml-3 cursor-pointer bg-green-500 rounded-lg hover:bg-white " 
                    @click="editQuestion"> 
                        <img src="@/assets/check.svg">
                    </div> -->
                </div>

            </div>

            <div class="m-4" >  
                <div class="bg-white font-bold text-black items-center text-left rounded-lg m-2" 
                v-for="(individualQuestion, index) in store.quizList[store.quizToLoad].questions" :key="index" >

                    <div class="flex text-2xl text-center p-2">

                        <div class="w-full text-left" v-if="!editingQuestion">
                            {{index + 1}}. {{individualQuestion.question}}  
                        </div>

                        <div class="w-full text-2xl text-left" v-if="editingQuestion" >
                            <input class="w-full" v-model="individualQuestion.question">  
                        </div>

                        <div class="w-20 flex justify-end">

                            <!-- <div>
                                <img v-show="store.userRole == 'Edit'" src="@/assets/trash.svg" class="cursor-pointer align-right">
                            </div> -->

                        </div>
                    </div>
                    
                    <div v-for="(answer, index) in individualQuestion.answers" :key="index" class="text-1xl pl-4 pb-4"
                     v-show="(store.userRole=='View' || store.userRole=='Edit') && store.showAnswer==true" >
                        <div class="w-full text-left" v-if="!editingQuestion">
                            {{store.indexToChar(index)}}) {{answer}}
                        </div>
                        
                        <div class="w-full text-2xl text-left" v-if="editingQuestion" >
                            <input class="w-full" v-model="individualQuestion.answers[index]">  
                        </div>


                    </div>

                </div>
                
                <div class="bg-green-500 font-bold hover:bg-white text-black items-center text-center text-4xl rounded-lg m-2 p-2 cursor-pointer" 
                @click="closeModal">
                    Close 
                </div>
            </div>
        </div>  
    </div>  
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { useStore } from '@/store/Store';


export default defineComponent({
  name: 'QuestionList',
  components: {
    
  },

  setup()
  {

    const store = useStore();
    store.getQuizzes();

    const editingQuestion = ref(false);
    const editingAnswer = ref(false);
    
    function closeModal(): void
    {
        store.questionModal = !store.questionModal;
        store.isEditingQuestion = false
    }

    function editQuestion()
    {
        store.isEditingQuestion = true
        store.newQuizModal = !store.newQuizModal;
    }

    function saveEditQuestion()
    {
        editingQuestion.value = !editingQuestion.value ;
        store.isEditingQuestion = !store.isEditingQuestion;
    }

    return {
      store,
      editingQuestion,
      editingAnswer,
      closeModal,
      editQuestion,
      saveEditQuestion
    }
  }
});
</script>

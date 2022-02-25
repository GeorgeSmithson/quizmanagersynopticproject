<template>
    <div class="absolute flex justify-center items-center w-screen h-screen z-4" >
        <div class="bg-etonblue rounded-lg px-14 py-8 shadow-lg container h-5/6 w-5/6 overflow-y-auto p-3 scrollbar scrollbar-thumb-custom scrollbar-track-custom-light">
            <div class="font-bold text-black text-center text-6xl"> 
              Select a Quiz
            </div>
            
            <div class="m-4 flex w-full" 
            v-for="quiz in store.quizList" :key="quiz.id"> 
                <div class="bg-white font-bold text-black items-center text-center text-5xl rounded-lg p-4 cursor-pointer w-full" 
                @click="store.loadQuestions(quiz.id)"> 
                  <div> 
                    {{quiz.title}} 
                  </div>
                </div>
                
                <div v-show="store.userRole =='Edit'" class="flex "  >
                      <img @click="store.deleteQuiz(quiz.id); store.logout(); reload(); " src="@/assets/trash.svg" class="cursor-pointer">
                </div>
            </div>

            <div v-show="store.userRole=='Edit'" 
            class="bg-green-500 font-bold hover:bg-white text-black items-center text-center text-5xl rounded-lg p-2 mr-7 ml-3 cursor-pointer" 
            @click="showNewQuizModal">
                Add New Quiz
            </div>

            <div 
            class="w-full flex items-center align-center justify-center" 
            @click="store.logout()">
                <img class="w-12 bg-white font-bold hover:bg-green-500 rounded-lg mt-5 cursor-pointer self-center" src="@/assets/login.svg">
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

    function showNewQuizModal(): void
    {
      store.newQuizModal = !store.newQuizModal;
    }

    //Bug & Workaround - Having to force page reload upon deleting quiz due to store.getQuizzes not refreshing data as it does in other functions/components
    function reload(): void
    {
      window.location.reload();
    }

    return {
      store,
      showNewQuizModal,
      reload,
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
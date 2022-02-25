import { createApp } from 'vue';
import App from './App.vue';
import './assets/tailwind.css';
import { createPinia } from 'pinia';
import Login from '@/components/NewQuestion.vue';
import QuizSelector from '@/components/QuizSelector.vue';
import QuestionList from '@/components/QuestionList.vue';
import NewQuiz from '@/components/NewQuiz.vue';
import NewQuestion from '@/components/NewQuestion.vue';

createApp(App)
.use(createPinia())
.component('Login', Login)
.component('QuizSelector', QuizSelector)
.component('QuestionList', QuestionList)
.component('NewQuiz', NewQuiz)
.component('NewQuestion', NewQuestion)

.mount('#app')

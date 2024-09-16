import { Answer, Question } from "@/types/Assessment";
import { useAssessmentContext } from "@/context/assessment";
import { FormEvent } from "react";


function Option({question, option}: {question: Question, option: Answer}) {
    const context = useAssessmentContext();
    if (!context) return;

    const {setAssessment} = context;
    function onSelectOption(e:FormEvent<HTMLElement>):void {
        setAssessment(prev => {
            const newState = Object.assign({}, prev);
            const currentQuestion = newState.questions.find(q => q.id === question.id);
            if (currentQuestion) {
                currentQuestion.selectedAnswer = option.id
            }
            return newState;
        })
    }

    return (
        <li style={{display: 'flex', gap:'5px', padding:"5px", cursor: 'pointer'}} >
            <input checked={question.selectedAnswer === option.id} onChange={onSelectOption} type="radio" id={option.id} name={question.id}/>
            <label style={{cursor: 'pointer'}} htmlFor={option.id}>{option.optionText}</label>
        </li>
    )
}

export default function GrammarQuestion({ question }: { question: Question }) {
    return (
        <div style={{display: "flex", flexDirection: 'column', gap:'15px'}}>
            <h2 style={{fontSize: "25px", fontWeight: "600"}}>{question.questionText}</h2>
            <ul>
                {question.answers.map(option => <Option option={option} question={question} key={option.id}/>)}
            </ul>
        </div>
    )
}
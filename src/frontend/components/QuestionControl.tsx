import { useAssessmentContext } from "@/context/assessment";
import { Button } from "antd";
import "./styles/_control.css"
import { TestTypes } from "@/constants";
import { useRouter } from "next/router";

export default function QuestionControl() {
    const router = useRouter();
    const context = useAssessmentContext();
    if (!context) throw new Error("Assessment not laoded");

    const { setAssessment, assessment } = context;

    function onPrev() {
        setAssessment(prev => {
            const newState = Object.assign({}, prev);
            if(!prev) return prev;
            if (prev.currentQuestion > 0) {
                newState.currentQuestion = prev.currentQuestion - 1
            }
            return newState
        })
    }
    function onNext() {
        setAssessment(prev => {
            const newState = Object.assign({}, prev);
            if (!prev) return prev
            if (prev.currentQuestion < prev.questions.length - 1) {
                newState.currentQuestion = prev.currentQuestion + 1
            }
            return newState
        })
    }

    function onSubmit() { 
        const results = assessment.questions.map(question => ({id: question.id, answer: question.selectedAnswer}))
        // TODO
        // fetch results

        const session = sessionStorage.getItem('ll__session');
        const sessionObject = JSON.parse(session || "false");

        if (assessment.testType === TestTypes.Grammar) {
            router.push("/assessments/listening");
        } else if (assessment.testType === TestTypes.Listening) {
            router.push(`/assessments/result?id=${sessionObject}`);
        }
    }

    return (
        <div className="control__wrapper">
            <div className="control__wrapper--inner">
                <Button type="primary" className="control__btn" onClick={onPrev} disabled={assessment.currentQuestion < 1} >Previous</Button>
                <Button type="primary" disabled={assessment.currentQuestion >= assessment.questions.length - 1} onClick={onNext} className="control__btn">Next</Button>
            </div>

            {
                assessment.currentQuestion >= assessment.questions.length - 1 ?
                    <Button className="control__submit control__btn" type="primary" onClick={onSubmit}>Submit</Button> :
                    null
            }
        </div>
    )
}
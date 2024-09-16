import QuestionList from "@/components/QuestionList";
import { TestTypes } from "@/constants";

export default function GrammarPage() {
    return <div style={{accentColor:"#5AFB7A", display: "contents"}}>
        <QuestionList testType={TestTypes.Grammar}/>
    </div>
}
import QuestionList from "@/components/QuestionList";
import { TestTypes } from "@/constants";

export default function ListeningPage() {
    return <div style={{accentColor:"#3155FF", display: "contents"}}>
        <QuestionList testType={TestTypes.Listening}/>
    </div>
}
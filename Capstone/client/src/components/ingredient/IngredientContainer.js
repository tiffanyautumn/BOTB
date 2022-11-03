import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button } from "reactstrap"
import { IngredientList } from "./IngredientList"
import { IngredientSearch } from "./IngredientSearch"

export const IngredientContainer = ({ isAdmin, isApproved }) => {
    const [searchTerms, setSearchTerms] = useState("")
    const navigate = useNavigate()
    return <>
        <section id="ingredients-header-section">
            <div className="ingredients-header">
                <h3 >Ingredients</h3>
                <IngredientSearch setterFunction={setSearchTerms} />
                {
                    isAdmin
                        ? <button id="createIngredient" className="btn" onClick={() => navigate('/ingredient/create')}>Create an Ingredient</button>
                        : ""
                }
            </div>
        </section>
        <IngredientList searchTermState={searchTerms} isAdmin={isAdmin} isApproved={isApproved} />


    </>
}
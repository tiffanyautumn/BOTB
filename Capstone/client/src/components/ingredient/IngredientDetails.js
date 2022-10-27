import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { Button, Card, CardBody, CardImg, Col, Table } from "reactstrap"
import { getIngredientById } from "../../modules/ingredientManager"
import { library, icon } from '@fortawesome/fontawesome-svg-core'
import './ingredient.css'
import { UseForm } from "./UseForm"
import { getUsesByIngredientId } from "../../modules/useManager"
import { getIngredientHazardByIngredientId } from "../../modules/ingredientHazardManager"
import { Hazard } from "./Hazard"
import { IngredientHazardForm } from "./IngredientHazardForm"
import { getAllHazards } from "../../modules/hazardManager"


export const IngredientDetails = ({ isAdmin, isApproved }) => {
    const { ingredientId } = useParams()
    const navigate = useNavigate()
    const [ingredient, setIngredient] = useState([])
    const [useForm, setUseForm] = useState(false)
    const [hazardForm, setHazardForm] = useState(false)
    const [uses, setUses] = useState([])
    const [ingredientHazards, setIngredientHazards] = useState([])
    const [hazards, setHazards] = useState([])
    const [hazardDelete, setHazardDelete] = useState(false)

    const getUses = () => {
        getUsesByIngredientId(ingredientId).then(u => setUses(u))
    }
    const getIngredient = () => {
        getIngredientById(ingredientId).then(i => setIngredient(i))
    }

    const getHazards = () => {
        getAllHazards().then(h => setHazards(h))
    }

    const getIngredientHazards = () => {
        getIngredientHazardByIngredientId(ingredientId).then(h => setIngredientHazards(h))
    }
    useEffect(
        () => {
            getIngredient()
            getUses()
            getIngredientHazards()
            getHazards()
        }, []
    )

    const ingredientReviewDisplay = () => {
        return <>
            <p>Our Review</p>
            <p>{ingredient?.ingredientReview?.rate?.rating}</p>
            <p>{ingredient?.ingredientReview?.review}</p>
            <p>{ingredient?.ingredientReview?.source}</p>
            <p>{ingredient?.ingredientReview?.userProfile?.firstName} {ingredient?.ingredientReview?.userProfile?.lastName}</p>
            <p>{ingredient?.ingredientReview?.dateReviewed}</p>
            {
                isApproved
                    ? <Button onClick={(() => navigate(`/ingredientReview/edit/${ingredient.ingredientReview.id}`))}>Edit Review</Button>
                    : ""
            }
            {
                isAdmin
                    ? <Button onClick={(() => navigate(`/ingredientReview/delete/${ingredient.ingredientReview.id}`))}>Delete Review</Button>
                    : ""
            }

        </>
    }
    return <>


        <div className="row justify-content-center">
            {/* <Card style={{ width: '18rem', height: '18rem' }}>

                {/* <CardImg top width="auto" src={ingredient?.imageUrl} alt="Card image cap" /> */}

            {/* </Card> */}
            <Card key={ingredient.id} style={{
                width: '75%'
            }}
            >
                <Col>
                    <CardBody>
                        <section className="ingredient-details">
                            <p className="Name panel-item">
                                <span className="title">{ingredient?.name}
                                    {isApproved ? <button className="btn" onClick={(() => navigate(`/ingredient/edit/${ingredient.id}`))}><i className="fa-solid fa-eye-dropper"></i></button> : ""}

                                    {isAdmin ? <button onClick={(() => navigate(`/ingredient/delete/${ingredient.id}`))} className="btn"><i class="fa-solid fa-trash-can"></i></button> : ""}
                                </span>

                            </p>
                            <p className="Uses panel-item">
                                <span className="title">Uses
                                    <button onClick={(() => setUseForm(true))} className="btn"><i className="fa-solid fa-plus"></i></button></span>
                                {
                                    useForm ? <UseForm setUseForm={setUseForm} ingredient={ingredient} getIngredient={getIngredient} getUses={getUses} /> : ""
                                }
                                {
                                    uses.map(
                                        (u) => <li>{u.description}</li>
                                    )
                                }
                            </p>
                            <p className="Hazards panel-item">
                                <span className="title">Hazards
                                    <button onClick={(() => setHazardForm(true))} className="btn"><i className="fa-solid fa-plus"></i></button>
                                    {isAdmin ? <button onClick={(() => setHazardDelete(!hazardDelete))} className="btn"><i class="fa-solid fa-trash-can"></i></button> : ""}

                                </span>
                                {hazardForm ? <IngredientHazardForm hazards={hazards} getIngredientHazards={getIngredientHazards} setHazardForm={setHazardForm} ingredient={ingredient} getIngredient={getIngredient} /> : ""}
                                {ingredientHazards.map((h) => (<Hazard hazard={h} hazardDelete={hazardDelete} getIngredientHazards={getIngredientHazards} />))}
                            </p>
                            <p className="Review panel-item">
                                <span className="title">Our Review {
                                    isApproved && !ingredient.ingredientReview
                                        ? <button onClick={(() => navigate(`/ingredientReview/create/${ingredientId}`))} className="btn"><i className="fa-solid fa-plus"></i></button>
                                        : ""
                                }</span>
                                {
                                    ingredient.ingredientReview
                                        ? ingredientReviewDisplay()
                                        : ""
                                }

                            </p>
                        </section>





                    </CardBody>
                </Col>
            </Card >

        </div>

    </>
}
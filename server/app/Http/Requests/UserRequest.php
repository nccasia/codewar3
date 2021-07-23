<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;
use Illuminate\Http\Exceptions\HttpResponseException;
use Illuminate\Http\JsonResponse;
use Illuminate\Validation\ValidationException;
use Illuminate\Contracts\Validation\Validator;

class UserRequest extends FormRequest
{
    /**
     * Determine if the user is authorized to make this request.
     *
     * @return bool
     */
    public function authorize()
    {
        return true;
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array
     */
    public function rules()
    {
        return [
            'user_name' => 'required',
            'first_name' => 'required',
            'second_name' => 'required',
            'linked_in_url' => 'required',
            'business_unit_id' => 'required|exists:bussinessunits,id',
        ];
    }

    public function messages()
    {
        return [
            'user_name.required' => 'user_name for the user is required.',
            'business_unit_id.required' => 'Business Unit is invalid.',
            'first_name.required' => 'First name for the user is required.',
            'second_name.required' => 'Second name for the user is required.',
        ];
    }
    public function failedValidation(Validator $validator)
    {
        $errors = (new ValidationException($validator))->errors();
        throw new HttpResponseException(response()->json(
            [
                'error' => $errors,
                'status_code' => 422,
            ],
            JsonResponse::HTTP_UNPROCESSABLE_ENTITY
        ));
    }
}
